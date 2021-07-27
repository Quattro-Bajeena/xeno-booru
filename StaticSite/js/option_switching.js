const edit_elem = document.getElementById("edit");
const comments_elem = document.getElementById("comments");

const edit_buttons = Array.from(document.getElementsByClassName("edit-post-button"));
const comments_buttons = Array.from(document.getElementsByClassName("comments-post-button"));


function switch_elements(event){
    
    if( comments_buttons.includes(event.target)){
        comments_elem.style.removeProperty("display");
        edit_elem.style["display"] = "none";
    }
    else if(edit_buttons.includes(event.target)){
        edit_elem.style.removeProperty("display");
        comments_elem.style["display"] = "none";
    }
    event.stopPropagation();
}


Array.from(document.getElementsByClassName("switch-button")).forEach(button => {
    button.addEventListener("click", switch_elements);
})

