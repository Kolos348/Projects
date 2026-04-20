module.exports = {
  apps: [{
    name: "service",
    script: "./service.js",
    watch: true,
    ignore_watch: ["node_modules", "history", "gem.md", "history/*", "prompt.md"],
    env: {
      NODE_ENV: "development",
    }
  }]
};
