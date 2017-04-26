"use strict";

var glob = require("glob");
var webpack = require('webpack');
var PROD = (process.env.NODE_ENV === 'production');

module.exports = {
    devtool: "source-map",
    entry: {
        //vendor: glob.sync("./libs/**/*.js"),
        app: './app/moneyManager.js'
    },
    //entry: './app/moneyManager.js',
    output: {
        devtoolLineToLine: true,
        sourceMapFilename: "./app.js.map",
        path: './dist',
        //filename: "[name].entry.js"
        filename: PROD ? 'app.min.js' : 'app.js'
    },
    plugins: PROD ? [
    new webpack.optimize.UglifyJsPlugin({
        compress: { warnings: false }
    })
    ] : [
        
    ],
    devServer: {
        contentBase: ".",
        host: "localhost",
        port: 9000
    },
    module: {
        loaders: [
            {
                test: /\.jsx?$/,
                loader: "babel-loader"
            }
        ]
    }
};