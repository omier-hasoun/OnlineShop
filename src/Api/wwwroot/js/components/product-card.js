class ProductCard extends HTMLElement
{
  // Runs automatically as soon as <product-card> is inserted into the page
  connectedCallback()
  {
    // Read custom attributes passed to the HTML tag
    const id = this.getAttribute('id');
    if (!id)
        throw new Error("product id missing");

    const title = this.getAttribute('title') || 'Default Item';
    const loading = this.getAttribute('loading') || 'lazy';
    const image = this.getAttribute('thumbnail');
    const brand = this.getAttribute('brand') || '';
    const inStock = this.getAttribute('inStock') === 'true';
    const textColor = inStock ? "green" : "red";
    const isAvailableText = inStock ? "In Stock" : "Unavailable";


    // Build the inner HTML for this custom element
    this.innerHTML = `
 <article class="product-card">
        <figure>
            <img src="${image}" alt="${title}" loading="${loading}">
        </figure>
        <div class="product-info">
            <p class="brand">${brand}</p>
            <h3><a class="product-title default-font" href="/products/${id}">${title}</a></h3>
            <div class="bottom-row">
                <div class="bottom-row-left">
                    ${this.createRating()}
                    <p class="product-availability" style="color:${textColor}">${isAvailableText}</p>
                </div>
                ${this.createPrice()}
            </div>
        </div>
    </article>
    `;

  }
createRating() {
    const rating = this.getAttribute('rating') || '0';

    return `
        <span class="rating">${rating}⭐</span>
    `;
}

createPrice() {

    const hasDiscount = this.getAttribute('hasDiscount') === 'true';
    const originalPrice = this.getAttribute('originalPrice');

    if (!hasDiscount)
    {

        if(!originalPrice)
            throw new Error("product does not have a price");

        return `
        <div class="price-section default-font">
            <p class="price">$${originalPrice}</p>
        </div>
        `;
    }

    const discountPrice = this.getAttribute('discountPrice');
    const discountPercentage = this.getAttribute('discountPercentage');

    return `
    <div class="price-section default-font">
        <span class="discount-percentage">${discountPercentage} % OFF</span>
        <del class="original-price">$${originalPrice}</del>
        <ins class="discount-price">$${discountPrice}</ins>
    </div>
    `;
}
}

// Register your tag name with the browser
customElements.define('product-card', ProductCard);
