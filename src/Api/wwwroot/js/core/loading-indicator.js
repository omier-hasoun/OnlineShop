export class LoadingIndicator {

    static show(element) {
        element.innerHTML = `
            <div class="loading">
                <span class="spinner">Loading...</span>
            </div>
        `;
    }

    static hide(element) {
        element.innerHTML = "";
    }
}
