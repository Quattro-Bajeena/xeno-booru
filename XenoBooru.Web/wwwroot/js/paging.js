

function change_posts_on_page(event){
    //let post_num = event.target.value;

    //window.localStorage["posts-on-page"] = post_num;
    this.form.submit();
}

const post_visible_input = document.getElementById("posts-on-page");
post_visible_input.addEventListener("change", change_posts_on_page);

const pending_input = document.getElementById("pending-checkbox");
pending_input.addEventListener("change", change_posts_on_page);


const url_params = new URLSearchParams(window.location.search);
if (url_params.has("onPage")) {
    post_visible_input.value = url_params.get("onPage");
}

if (url_params.has("showPending")) {
    pending_input.checked = true;
}