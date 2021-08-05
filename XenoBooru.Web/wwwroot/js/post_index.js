const pending_checkbox = document.getElementById("pending-checkbox");

const posts = Array.from(document.getElementById("posts").children);

function post_pending(post){
    return post.querySelector('img').classList.contains("pending-True");
}

function toggle_pending(event){
    const show = event.target.checked;

    for(let post of posts){

        if(show == false && post_pending(post)){
            post.style.display = "none";
        }
        else{
            post.style.display = null;
        }
    }
}

pending_checkbox.addEventListener("change", toggle_pending);
toggle_pending({target : {checked : false}});