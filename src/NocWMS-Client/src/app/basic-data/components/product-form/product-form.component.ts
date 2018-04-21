import { Component, OnInit } from '@angular/core';
import { Product } from '../../../models/product';
import { Router, ActivatedRoute } from '@angular/router';
import { ProductService } from '../../../services/product/product.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'noc-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})
export class ProductFormComponent implements OnInit {
  public product: Product = new Product();
  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private productService: ProductService,
    private toastrService: ToastrService) {
  }

  ngOnInit(): void {
    this.activatedRoute.queryParams
      .subscribe(params => {
        this.product.categoryId = params.id;
      });
  }

  public onSubmit() {
    this.productService.add(this.product)
      .subscribe(data => {
        if (data.isSuccess) {
          this.toastrService.success("添加产品成功", "成功");
        } else {
          this.toastrService.error("添加产品失败: " + data.message, "失败");
        }
      });
  }

  public backToTable() {
    this.router.navigate(['../product-table'], {
      relativeTo: this.activatedRoute,
      queryParams: { "id": this.product.categoryId }
    });
  }
}
