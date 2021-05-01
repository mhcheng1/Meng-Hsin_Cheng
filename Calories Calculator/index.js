
if (document.readyState =='loading') {
    document.addEventListener('DOMContentLoaded', ready)
}
else {
    ready()
}

function ready() {

    // remove button
    var removeCartItemButtons = document.getElementsByClassName('btn-danger')
    console.log(removeCartItemButtons)
    for (var i = 0; i < removeCartItemButtons.length; i++) {
        var button = removeCartItemButtons[i]
        button.addEventListener("click",removeItem)
    }
    
    var quantityInput = document.getElementsByClassName('cart-quantity-input')
    for (var i = 0; i < quantityInput.length; i++) {
        var input = quantityInput[i]
        input.addEventListener('change', quantityChanged)
    }

    var addCartButtons = document.getElementsByClassName('shop-item-button')
    for (var i = 0; i < addCartButtons.length; i++) {
        var button = addCartButtons[i]
        button.addEventListener('click', addItemToCart)
    }

    document.getElementsByClassName('btn-purchase')[0].addEventListener('click', purchaseClicked)
}

function purchaseClicked (event) {
    alert('Thank you for using')
    var cartItems = document.getElementsByClassName('cart-items')[0]
    while (cartItems.hasChildNodes()) {
        cartItems.removeChild(cartItems.firstChild)
    }
    updateCartTotal()
}

function removeItem (event) {
    var buttonClicked = event.target
    buttonClicked.parentElement.parentElement.remove()
    updateCartTotal()
}


function addItemToCart (event) {
    var item = event.target.parentElement.parentElement
    var title = item.getElementsByClassName('shop-item-title')[0].innerText
    var image = item.getElementsByClassName('shop-item-image')[0].src
    var price = item.getElementsByClassName('shop-item-price')[0].innerText
    addItemToCartRow(title, image, price)
    updateCartTotal()
}

function addItemToCartRow (title, image, price) {
    var cartRow = document.createElement('div')
    var cartItems = document.getElementsByClassName('cart-items')[0]
    var cartItemTitles = cartItems.getElementsByClassName('cart-item-title')

    for (var i = 0; i < cartItemTitles.length; i++){
        if (cartItemTitles[i].innerText == title) {
            alert('This is already in the cart')
            return
        }
    }

    cartRow.classList.add('cart-row')
    var cartContents = `
        <div class="cart-item cart-column">
            <img class="cart-item-image" src="${image}" width="100" height="100">
            <span class="cart-item-title">${title}</span>
        </div>
        <span class="cart-price cart-column">${price}</span>
        <div class="cart-quantity cart-column">
            <input class="cart-quantity-input" type="number" value="1">
            <button class="btn btn-danger" type="button">REMOVE</button>
        </div>`
    cartRow.innerHTML = cartContents
    cartItems.append(cartRow)
    
    // check for removal
    cartRow.getElementsByClassName('btn-danger')[0].addEventListener('click', removeItem)
    cartRow.getElementsByClassName('cart-quantity-input')[0].addEventListener('change', quantityChanged)
}

// Input changed event
function quantityChanged(event) {
    var input = event.target
    if (isNaN(input.value) || input.value <= 0) {
        input.value = 1
    }
    updateCartTotal()
}

function updateCartTotal() {
    var cartItemContainer = document.getElementsByClassName('cart-items')[0]
    var cartRows = cartItemContainer.getElementsByClassName('cart-row')
    var total = 0
    for (var i = 0; i < cartRows.length; i++) {
        var cartRow = cartRows[i]
        var priceElement = cartRow.getElementsByClassName('cart-price')[0]
        var quantityElement = cartRow.getElementsByClassName('cart-quantity-input')[0]

        var price = parseFloat(priceElement.innerText)
        var quantity = quantityElement.value
        total += price * quantity
    }
    total = Math.round (total * 100) / 100
    document.getElementsByClassName('cart-total-price')[0].innerText = total + ' Calories'
}