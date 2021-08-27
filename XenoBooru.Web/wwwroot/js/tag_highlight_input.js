function highlight_input_tags(tag_input_styled, tags_existing, event) {
    let tags_str = event.target.value.trim().split(/\s+/);
    //let tags_str = event.target.value.split(' ');
    let elements = new Array;
    tag_input_styled.innerHTML = '';

    for(let tag_str of tags_str){
        
        let tag_existing = tags_existing.find(tag => tag.name == tag_str);
        
        let type;
        if(tag_existing)
            type = tag_existing.type;
        else
            type = "NotFound";

        //console.log(tag_str, type);

        let span = document.createElement("span");
        span.classList.add("tag-input-" + type);
        span.innerHTML = tag_str;

        elements.push(span);
        tag_input_styled.appendChild(span);
        tag_input_styled.innerHTML += " ";

    }
    //event.target.innerHTML = "lol";
    //event.target.value = elements;
    //console.log(elements);
    
}


fetch(get_tags_url)
    .then(response => response.json())
    .then(data => {
        let tag_input_styled = document.getElementById("tag-input-styled");
        let tag_input = document.getElementById("tag-input");

        tag_input.addEventListener("input", event => {
            highlight_input_tags(tag_input_styled, data, event);
        });

        highlight_input_tags(tag_input_styled, data, {target: tag_input});
        
    });


