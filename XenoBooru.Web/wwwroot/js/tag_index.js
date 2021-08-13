const name_input = document.getElementById("tag-name");
const type_input = document.getElementById("tag-type");
const order_input = document.getElementById("tag-order");

const url_params = new URLSearchParams(window.location.search);

console.log(url_params.get("name"));
console.log(url_params.get("type"));
console.log(url_params.get("order"));

const query = url_params.get("name");
const type = url_params.get("type");
const order = url_params.get("order");

if(query != null){
    name_input.value = query;
}

if(type != null){
    type_input.value = type;
}

if(order != null){
    order_input.value = order;
}
