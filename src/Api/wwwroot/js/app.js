
import '/js/components/app-header.js';
import '/js/components/app-footer.js';
import { Router } from './core/router.js';
import { PageLoader } from './core/page-loader.js';

const searchFormInput = document.getElementById("searchFormInput");
const submitQueryBtn = document.getElementById("searchFormSubmit");
// const content = document.getElementById("content");
const router = new Router();
const pageLoader = new PageLoader();

const size = 25;

searchFormInput.addEventListener("input", () => {
    const hasText = searchFormInput.value.trim().length >= 1;
    submitQueryBtn.disabled = !hasText;
});


submitQueryBtn.addEventListener("click", async (event) => {
    event.preventDefault();

    const page = 1;
    const filters = {
        q: searchFormInput.value.trim(),
        size: size,
        page: page,
    }

    const url = new URL('/products.html', window.location.origin);
    url.search = new URLSearchParams(filters);

    await router.navigate(url);

});

async function initializePage() {

    changeSearchInputFromUrl();
    await pageLoader.load(window.location.href);
    const currentPage = document.body.getAttribute("data-page");

    const pageModule = await import(`/js/pages/${currentPage}.js`);

    await pageModule.init();
}

function changeSearchInputFromUrl()
{
    searchFormInput.value = new URLSearchParams(location.search).get('q');
}

document.addEventListener("DOMContentLoaded", initializePage);

window.addEventListener("popstate", initializePage);
document.addEventListener("pageLoaded", initializePage);
