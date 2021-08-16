/// <reference path='../lib/three/src/Three.js' />
// import * as THREE from "https://threejs.org/build/three.module.js";
// import { OrbitControls } from "https://threejs.org/examples/jsm/controls/OrbitControls.js";
// import { GLTFLoader } from "https://threejs.org/examples/jsm/loaders/GLTFLoader.js";

import * as THREE from "/lib/three/build/three.module.js";
import { MapControls, OrbitControls } from "/lib/three/examples/jsm/controls/OrbitControls.js";
import { GLTFLoader } from "/lib/three/examples/jsm/loaders/GLTFLoader.js";

let level = 729;
let camera, scene, renderer, map, controls;
const parent = document.getElementById("render");

const render_controls = document.getElementById("render-controls");
const texture_filtering_checkbox = document.getElementById("texture-filtering-checkbox");
const fullscreen_checkbox = document.getElementById("fullscreen-checkbox");
const wireframe_checkbox = document.getElementById("wireframe-checkbox");
const controls_list = document.getElementById("controls-list");


//console.log(map_url);

let minDistance = 500;
let maxDistance = 7000;

try {
    init();
    render();
} catch (error) {
    console.error(error);
    console.log("Couldn't initialize 3D viewer");
}



function init(){

    const width = parent.clientWidth;
    const height = parent.clientHeight;

    renderer = new THREE.WebGLRenderer();
    renderer.setPixelRatio( window.devicePixelRatio );
    renderer.setSize( width, height );
    renderer.outputEncoding = THREE.sRGBEncoding;
    parent.appendChild(renderer.domElement);

    camera = new THREE.PerspectiveCamera( 60, width / height, 10, 20000 );
    camera.position.set( 2500, 500, 400 );
    camera.lookAt( 0, 0, 0 );

    scene = new THREE.Scene();

    const loadingManager = new THREE.LoadingManager(function(){
        scene.add(map);
    })

    const loader = new GLTFLoader( loadingManager);
    loader.load(map_url, function(gltf){
        //console.log(gltf);
        
        for(const object of gltf.scene.children){
            
            object.traverse(function(child){
                if(child instanceof THREE.Mesh){

                    child.material.emissive = new THREE.Color( 1,1,1);
                    child.material.emissiveMap = child.material.map;
                    child.material.emissiveIntensity = 1;

                }

            });
        }
        map = gltf.scene;
        render();
    });


    controls= new OrbitControls( camera, renderer.domElement );
    controls.minDistance = minDistance;
    controls.maxDistance = maxDistance;
    controls.target.set( 0, 0, 0 );
    controls.update();

   

    window.addEventListener( 'resize', onWindowResize );
    texture_filtering_checkbox.addEventListener('change', toggleTextureFiltering);
    fullscreen_checkbox.addEventListener('change', fullScreenToggle);
    wireframe_checkbox.addEventListener("change", toggleWireframe);
    document.addEventListener("keydown", fullScreenToggle);
    controls_list.addEventListener("change", changeControls);


    render();
    return true;
}


function onWindowResize() {
    const width = parent.clientWidth;
    const height = parent.clientHeight;

    camera.aspect = width / height;
    camera.updateProjectionMatrix();
    renderer.setSize( width, height );
    render();
    
}


function render() {
    requestAnimationFrame( render );
    resizeCanvasToDisplaySize();
    controls.update();
    renderer.render( scene, camera );
    
}

function resizeCanvasToDisplaySize() {
    const canvas = renderer.domElement;
    // look up the size the canvas is being displayed
    const width = canvas.clientWidth;
    const height = canvas.clientHeight;
  
    // adjust displayBuffer size to match
    if (canvas.width !== width || canvas.height !== height) {
      // you must pass false here or three.js sadly fights the browser
      renderer.setSize(width, height, false);
      camera.aspect = width / height;
      camera.updateProjectionMatrix();
  
      // update any render target sizes here
    }
}

function changeControls(event){
    if(event.target.value == "orbit"){
        setUpOrbitContols(minDistance, maxDistance);
    }
    else if(event.target.value == "map"){
        setUpMapControls(minDistance, maxDistance);
    }
}

function setUpOrbitContols(min_distance, max_distance){
    const target = controls.target;
    controls.dispose();
    const orbitControls = new OrbitControls( camera , renderer.domElement);
    orbitControls.minDistance = min_distance;
    orbitControls.maxDistance = max_distance;
    orbitControls.target = target;

    controls = orbitControls;
    controls.update();
}

function setUpMapControls(min_distance, max_distance){

    const target = controls.target;
    controls.dispose();
    const mapControls = new MapControls( camera , renderer.domElement);

    mapControls.enableDamping = false; // an animation loop is required when either damping or auto-rotation are enabled
    mapControls.dampingFactor = 0.05;

    mapControls.screenSpacePanning = false;

    mapControls.minDistance = min_distance;
    mapControls.maxDistance = max_distance;

    mapControls.maxPolarAngle = Math.PI / 2;
    mapControls.target = target;

    
    controls = mapControls;
    controls.update();
}





function toggleTextureFiltering(event){
    for(const object of map.children){  
        object.traverse(function(child){
            if(child instanceof THREE.Mesh){

                if(child.material.map != null){

                    if(event.currentTarget.checked){
                        child.material.map.magFilter = THREE.LinearFilter;
                        child.material.map.minFilter = THREE.LinearMipMapLinearFilter; 
                    }
                    else{
                        child.material.map.magFilter = THREE.NearestFilter;
                        child.material.map.minFilter = THREE.NearestFilter;
                    }
                    
                    child.material.map.needsUpdate = true;
                    child.material.needsUpdate = true;
                }
            }

        });
    }
}

function toggleWireframe(event) {
    for (const object of map.children) {
        object.traverse(function (child) {
            if (child.isMesh && child.material.map != null) {

                if (event.currentTarget.checked) {
                    child.material.wireframe = true;
                }
                else {
                    child.material.wireframe = false;
                }

                child.material.map.needsUpdate = true;
                child.material.needsUpdate = true;

            }
        });

    }
}

function fullScreenToggle(event) {
    

    if(event.currentTarget.checked == true ||
        (event.key == "Escape" && fullscreen_checkbox.checked == false)
        ){
        
        parent.style.position = "fixed";
        parent.style.left = "0";
        parent.style.top = "0";
        parent.style.height = "99vh";
        parent.style.width = "99vw";
        renderer.domElement.style.width = "100%";
        renderer.domElement.style.height = "100%";

        render_controls.style.fontSize = "1.2em";
        fullscreen_checkbox.checked = true;


        resizeCanvasToDisplaySize();
    }
    else if(event.currentTarget.checked == false || 
        (event.key == "Escape" && fullscreen_checkbox.checked == true)
        ){

        parent.removeAttribute("style");
        render_controls.removeAttribute("style");
        fullscreen_checkbox.checked = false;

        resizeCanvasToDisplaySize();
    }
    
    
};

