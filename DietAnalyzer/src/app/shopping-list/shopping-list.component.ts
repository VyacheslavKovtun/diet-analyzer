import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ShoppingListService } from '../common/api/services/shopping-list.service';
import { Aisle, Item } from '../common/interfaces/aisle.interface';
import { ShoppingItem } from '../common/interfaces/shopping-item.interface';
import { AddShoppingListItemDialog } from '../shared/dialogs/add-shopping-list-item-dialog/add-shopping-list-item-dialog';
import { AddShoppingListProductDialog } from '../shared/dialogs/add-shopping-list-product-dialog/add-shopping-list-product-dialog';

@Component({
  selector: 'app-shopping-list',
  templateUrl: './shopping-list.component.html',
  styleUrls: ['./shopping-list.component.css']
})
export class ShoppingListComponent implements OnInit {
  food: Aisle[] = [];
  noFood: Aisle[] = [];

  aSLIDialog !: MatDialogRef<AddShoppingListItemDialog, any>;
  itemName!: string;

  addingItem !: ShoppingItem;

  constructor(public dialog: MatDialog, private shoppingListService: ShoppingListService) {
    this.loadList();
  }

  ngOnInit(): void {
  }

  loadList() {
    this.shoppingListService.getShoppingList().subscribe(res => {
      this.food = [];
      this.noFood = [];

      res.forEach(r => {
        if(r.aisle != "Non-Food Items") {
          this.food.push(r);
        }
        else {
          this.noFood.push(r);
        }
      });
    });
  }

  addFood() {
    const dialogRef = this.dialog.open(AddShoppingListProductDialog);

    dialogRef.afterClosed().subscribe(result => {
      this.loadList();
    });
  }

  bought(item: Item) {
    this.shoppingListService.deleteFromShoppingList(item.id).subscribe(() => {
      this.loadList();
    });
  }

  addItem() {
    this.aSLIDialog = this.dialog.open(AddShoppingListItemDialog, {
      width: '350px',
      data: {itemName: this.itemName}
    });

    this.aSLIDialog.afterClosed().subscribe(result => {
      this.itemName = result[0];

      this.addingItem = {
        item: this.itemName,
        parse: false
      };

      this.shoppingListService.addToShoppingList(this.addingItem).subscribe(() => {
        this.loadList();
      });
    });
  }
}
