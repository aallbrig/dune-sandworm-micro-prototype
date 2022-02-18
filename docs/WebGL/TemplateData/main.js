var canvas = document.querySelector("#unity-canvas");

var config = {
    dataUrl: "Build/WebGL.data",
    frameworkUrl: "Build/WebGL.framework.js",
    codeUrl: "Build/WebGL.wasm",
    streamingAssetsUrl: "StreamingAssets",
    companyName: "Allbright Web Services LLC",
    productName: "dune-sandworm-micro-prototype",
    productVersion: "0.0.60",
    devicePixelRatio: 1,
}

createUnityInstance(canvas, config);
