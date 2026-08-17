class AppHeader extends HTMLElement
{
    connectedCallback()
    {
        this.innerHTML =  `
           <div
        `;
    }
}

customElements.define('app-header', AppHeader);
