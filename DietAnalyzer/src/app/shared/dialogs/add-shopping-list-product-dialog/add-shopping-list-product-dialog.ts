import { Component, OnInit } from "@angular/core";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { MatDialogRef, MatDialog } from "@angular/material/dialog";
import { ProductsService } from "src/app/common/api/services/products.service";
import { ShoppingListService } from "src/app/common/api/services/shopping-list.service";
import { ShoppingItem } from "src/app/common/interfaces/shopping-item.interface";
import { ShortProduct } from "src/app/common/interfaces/short-product.interface";
import { EmptyFieldDialog } from "../empty-field-dialog/empty-field-dialog";

@Component({
  selector: 'add-shopping-list-product-dialog',
  templateUrl: 'add-shopping-list-product-dialog.html',
  styleUrls: ['add-shopping-list-product-dialog.css']
})
export class AddShoppingListProductDialog implements OnInit{
  findProductForm !: FormGroup;
  products : ShortProduct[] = [];

  addingProd !: ShoppingItem;

  constructor(public dialogRef: MatDialogRef<AddShoppingListProductDialog>, private productsService: ProductsService,
    public dialog: MatDialog, private shoppingListService: ShoppingListService) {

  }

  addProduct(product: ShortProduct) {
      if(product) {
        this.addingProd = {
          id: product.id,
          item: product.title,
          parse: true
        };
    
        this.save();
      }
  }

  save() {
    if(this.addingProd) {
      this.shoppingListService.addToShoppingList(this.addingProd).subscribe();
    }
  }

  ngOnInit(): void {
    this.findProductForm = new FormGroup({
      "productTitle": new FormControl('', Validators.required)
    });
  }

  searchClick() {
    const field = this.findProductForm.value;
    var title = field.productTitle;
    
    if(title != '') {
      this.products = [];

      this.productsService.getProductsByTitle(title).subscribe(res => {
        this.products = res;
      });
    }
    else {
      this.dialog.open(EmptyFieldDialog);
    }
  }

  onCancelClick(): void {
    this.dialogRef.close();
  }
}