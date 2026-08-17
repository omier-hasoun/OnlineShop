import { PageLoader } from "./page-loader.js";

export class Router {

    #loader = new PageLoader();

    constructor()
    {

    }

  /**
   * @param {URL | String}  url
   */
   async navigate(url, forceReload = false) {

    if (forceReload) {
      window.location.href = url;
      return;
    }

    const href = url instanceof URL ? url.href : url;

    await this.#loader.load(href);
    history.pushState(null, '', href);

    document.dispatchEvent(new CustomEvent("pageLoaded"));
  }
}
