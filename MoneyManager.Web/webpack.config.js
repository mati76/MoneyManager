"use strict";

var glob = require("glob");
var webpack = require('webpack');
var PROD = (process.env.NODE_ENV === 'production');

module.exports = {
    //entry: {
    //    //js: glob.sync("./app/components/**/*.js")
    //    calendar: './app/components/calendar/calendar.js',
    //    auth: './app/components/auth/auth.js',
    //    category: './app/components/category/category.js',
    //    login: './app/components/login/login.js',
    //    expense: './app/components/expense/expense.js',
    //    income: './app/components/income/income.js',
    //    home: './app/components/home/home.js',
    //    budget: './app/components/budget/budget.js',
    //    shared: './app/shared/shared.js'
    //},
    entry: './app/moneyManager.js',
    output: {
        path: './dist',
        //filename: "[name].entry.js"
        filename: PROD ? 'bundle.min.js' : 'bundle.js'
    },
    plugins: PROD ? [
    new webpack.optimize.UglifyJsPlugin({
        compress: { warnings: false }
    })
    ] : [],
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