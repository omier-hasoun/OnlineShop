class AppHeader extends HTMLElement
{
    connectedCallback()
    {
        this.innerHTML =  `
            <header>
    <nav id="mainMenuNav" class="default-font" aria-label="main-menu" >
        <ul >
            <li><a href="#home">Home</a></li>
            <li><a href="#orders">Orders</a></li>
            <li><a href="#profile">Profile</a></li>
        </ul>
    </nav>
    <form id="searchForm" class="default-font">
            <input id="searchFormInput" type="search" name="q" placeholder="Search products..." maxlength="100" required>
            <button id="searchFormSubmit" type="submit" disabled>&#128269</button>
    </form>
</header>
        `;
    }
}

customElements.define('app-header', AppHeader);
