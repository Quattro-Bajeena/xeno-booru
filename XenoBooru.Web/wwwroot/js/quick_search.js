const debounce = (func, wait) => {
    let timeout;

    return function executedFunction(...args) {
        const later = () => {
            timeout = null;
            func(...args);
        };

        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
};


const searchbar = document.getElementById("search");
const search_input = searchbar.querySelector(".search-bar");

const tag_completion = document.getElementById("tag-completion-box");
tag_completion.addEventListener('click', pick_completion);

const MAX_MATCHES = 10;


let tags_all;
fetch(get_tags_url)
    .then(response => response.json())
    .then(data => {
        //console.log(data);
        tags_all = data;
        search_input.addEventListener("input", debounce(quick_search, 300));
    });

function quick_search(event){
    //console.log(event.target.value);
    const last_match = event.target.value.toLowerCase().split(' ').pop();
    if(last_match.length == 0){
        tag_completion.style.display = "none";
        return;
    }
        

    const matched_tags = tags_all.filter(tag => tag.name.includes(last_match)).sort( (tag1, tag2) => tag1.postCount < tag2.postCount).slice(0, MAX_MATCHES);

    const ul = tag_completion.querySelector("ul");
    ul.textContent = '';

    for (const matched_tag of matched_tags){
        //console.log(matched_tag);
        const li = document.createElement("li");
        li.classList.add("tag");
        li.classList.add("tag-type-" + matched_tag.type);
        li.textContent = matched_tag.name;

        ul.appendChild(li);
    }

    tag_completion.style.display = null;

}



function pick_completion(event){
    let queries = search_input.value.split(' ');
    queries.pop();
    queries.push(event.target.innerText);
    search_input.value = queries.join(' ') + " ";
    tag_completion.style.display = "none";
}






