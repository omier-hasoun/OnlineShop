class AppFooter extends HTMLElement
{
    connectedCallback()
    {
        this.innerHTML = `
    <footer>
        Copyright reserved by omier 2026
    </footer>
        `;
    }
}

customElements.define('app-footer', AppFooter);
