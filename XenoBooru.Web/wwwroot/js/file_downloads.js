function registerDownload(event){
    const name = event.target.id;
    let options = {
        method: 'POST'
    }
    fetch("/Home/RegisterFileDownload/?name=" + name, options)
        .then(response => response.json())
        .then(data => {
            console.log("Downloaded " + name);
        });
}

Array.from(document.querySelectorAll(".download-link"))
    .forEach(link => link.addEventListener('click', registerDownload ));

