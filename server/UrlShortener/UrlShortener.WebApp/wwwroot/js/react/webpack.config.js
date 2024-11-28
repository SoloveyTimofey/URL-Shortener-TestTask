//console.log("Building with Webpack...");

//module.exports = {
//    context: __dirname,
//    entry: "./app.js",
//    output: {
//        path: __dirname + "/dist",
//        filename: "bundle.js"
//    },
//    mode: "development",
//    watch: true,
//    module: {
//        rules: [
//            {
//                test: /\.(js|jsx)$/,
//                exclude: /(node_modules)/,
//                use: {
//                    loader: 'babel-loader',
//                    options: {
//                        presets: ['@babel/preset-env', '@babel/preset-react']
//                    }
//                }
//            }
//        ]
//    }
//}

console.log("Building with Webpack...");

module.exports = {
    context: __dirname,
    entry: "./app.js",
    output: {
        path: __dirname + "/dist",
        filename: "bundle.js"
    },
    mode: "development",
    watch: true,
    module: {
        rules: [
            {
                test: /\.(js|jsx)$/,
                exclude: /(node_modules)/,
                use: {
                    loader: 'babel-loader',
                    options: {
                        presets: ['@babel/preset-env', '@babel/preset-react']
                    }
                }
            },
            {
                test: /\.css$/,
                use: ['style-loader', 'css-loader']
            }
        ]
    }
}
