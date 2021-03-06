module.exports = {
  //Development server and proxy
  server: {
    proxy: {
      url: 'https://private-843814-schackaton.apiary-mock.com/api/',
      route: '/api/'
    },
    publicPath: 'js'
  },

  //Critical Css
  enableCriticalCss: false,

  //Directory names
  dir: {
    source: 'src',
    development: 'dev',
    production: 'dist',
    documentation: 'docs',
    static: 'static',
    html: 'html',
    stories: 'src/stories',
    hbsHelpers: 'gulp/handlebars',
    assets: {
      javascript: 'js',
      css: 'css',
      scss: 'scss',
      images: 'images',
      fonts: 'fonts',
      htmlTemplates: 'templates',
      htmlModules: 'modules',
      htmlComonents: 'components'
    },

    //Asset paths
    get paths() {
      return {
        //js
        srcJS: `${this.source}/${this.assets.javascript}`,
        //statics
        srcStatic: `${this.source}/${this.static}`,
        //fonts
        srcFonts: `${this.source}/${this.static}/${this.assets.fonts}`,
        devFonts: `${this.development}/${this.assets.fonts}`,
        distFonts: `${this.production}/${this.assets.fonts}`,
        //images
        srcImages: `${this.source}/${this.static}/${this.assets.images}`,
        devImages: `${this.development}/${this.assets.images}`,
        distImages: `${this.production}/${this.assets.images}`,
        //styles
        srcStyles: `${this.source}/${this.assets.scss}`,
        devStyles: `${this.development}/${this.assets.css}`,
        distStyles: `${this.production}/${this.assets.css}`,
        //html
        srcHtml: `${this.source}/${this.html}`,
        //templates
        srcTemplates: `${this.source}/${this.html}/${this.assets.htmlTemplates}`,
        devTemplates: `${this.development}/${this.html}/${this.assets.htmlTemplates}`,
        distTemplates: `${this.production}/${this.html}/${this.assets.htmlTemplates}`,
        //modules
        srcModules: `${this.source}/${this.html}/${this.assets.htmlModules}`,
        devModules: `${this.development}/${this.html}/${this.assets.htmlModules}`,
        distModules: `${this.production}/${this.html}/${this.assets.htmlModules}`,
        //components
        srcComponents: `${this.source}/${this.html}/${this.assets.htmlComonents}`,
        devComponents: `${this.development}/${this.html}/${this.assets.htmlComonents}`,
        distComponents: `${this.production}/${this.html}/${this.assets.htmlComonents}`
      };
    }
  }
};
