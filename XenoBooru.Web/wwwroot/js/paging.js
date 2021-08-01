

function change_posts_on_page(event){
    //let post_num = event.target.value;

    //window.localStorage["posts-on-page"] = post_num;
    this.form.submit();
}

const post_visible_input = document.getElementById("posts-on-page");
post_visible_input.addEventListener("change", change_posts_on_page);

let url_params = new URLSearchParams(window.location.search);
if(url_params.has("postsOnPage")){
    post_visible_input.value = url_params.get("postsOnPage");
}