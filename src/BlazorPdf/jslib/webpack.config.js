const path = require("path");

module.exports = {
  entry: './src/index.ts',
  module: {
    rules: [
      {
        test: /\.ts?$/,
        use: 'ts-loader',
        exclude: /node_modules/,
      },
    ],
  },
  resolve: {
    extensions: ['.tsx', '.ts', '.js'],
  },
  optimization: {
    usedExports: true, // Enable tree shaking
  },
  experiments: {
    outputModule: true,
  },
  output: {
    path: path.resolve(__dirname, '../wwwroot/js'),
    filename: "bundle.js",
    clean: true,
    library: {
      type: "module",
    },
  },
};
