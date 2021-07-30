

function highlight_posts_with_tag(selected_tag) {
    for (var post of posts) {
        let post_elem = document.getElementById("post-" + post["id"]);


        if (selected_tag && post["tags"].filter(tag => tag["name"] == selected_tag).length > 0) {
            console.log(post_elem);
            post_elem.classList.add("highlighted-post");
        }
        else {
            post_elem.classList.remove("highlighted-post");
        }
    }
}

tags.forEach(tag => {
    let tag_elem = document.getElementById("tag-" + tag["name"]);
    let link_elem = tag_elem.querySelector("a");

    console.log(tag["name"]);
    link_elem.addEventListener("mouseover", event => {
        highlight_posts_with_tag(tag["name"]);
    });
    link_elem.addEventListener("mouseout", event => {
        highlight_posts_with_tag(null);
    });
});

console.log(posts);
console.log(tags);