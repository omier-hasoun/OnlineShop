
export class PageLoader {

    #parser = new DOMParser();
    #contentContainer = document.getElementById("content");
    constructor() {

    }
/**
 * @param {string} pageUrl
 */
    async load(pageUrl)
    {
        const response = await fetch(pageUrl);

        if (!response.ok) {
            throw new Error(`Failed to load page: ${response.status}`);
        }
        const html = await response.text();

        const parsedHtml = this.#parser.parseFromString(html, "text/html");
        const content = parsedHtml.getElementById("content");
        document.body.setAttribute("data-page", parsedHtml.body.getAttribute("data-page"));


        if (!content) {
            throw new Error("Page does not contain #content");
        }
        this.#contentContainer.innerHTML = content.innerHTML;
    }

}
