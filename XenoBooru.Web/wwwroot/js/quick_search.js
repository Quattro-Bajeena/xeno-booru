console.log(get_tags_url);


const searchbar = document.getElementById("search");
const search_input = searchbar.querySelector("input");

console.log(searchbar, search_input);

let tags_all;
fetch(get_tags_url)
    .then(response => response.json())
    .then(data => {
        console.log(data);
        tags_all = data;

        search_input.addEventListener("input", event => {
            console.log(event.target.value);
            debounce( () => quick_search(event), 50);
        });
    });

function quick_search(event){
    console.log(event.target.value);
}


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