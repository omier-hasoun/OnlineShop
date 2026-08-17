import { Product } from "/js/core/models/product.js";


export class ProductClient
{
    constructor() {

    }
/**
 * @typedef {Object} ProductsResult
 * @property {Product[]} products
 * @property {number} page
 * @property {number} size
 * @property {boolean} hasMore
 */

/**
 * @param {string} query
 * @param {number} page
 * @param {number} size
 * @returns {Promise<ProductsResult>}
 */
    async getProducts(query, page=1, size=25)
    {
        const url = new URL( '/api/products', window.location.origin);
        url.searchParams.append('page', page);
        url.searchParams.append('size', size);
        url.searchParams.append('searchQuery', query);


        const response = await fetch(url);
        const json = await response.json();
        return {
            products: json.items.map((item) => new Product(item)),
            page: json.page,
            size: json.size,
            hasMore: json.hasMore
        };
    }

}
