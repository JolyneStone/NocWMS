import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { OutboundReceiptService } from '../../../services/outbound-receipt/outbound-receipt.service';
import { ToastrService } from 'ngx-toastr';
import { OutboundReceipt } from '../../../models/outbound-receipt';

@Component({
  selector: 'out-outbound-receipt-detail',
  templateUrl: './outbound-receipt-detail.component.html',
  styleUrls: ['./outbound-receipt-detail.component.css']
})
export class OutboundReceiptDetailComponent implements OnInit {
  public outboundReceipt: OutboundReceipt;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private outboundReceiptService: OutboundReceiptService,
    private toastrService: ToastrService,
  ) { }

  ngOnInit() {
    this.activatedRoute.queryParams
      .subscribe(params => {
        let id = params.id;
        this.outboundReceiptService.getOutboundReceipt(id)
          .subscribe(data => {
            if (data.isSuccess) {
              this.outboundReceipt = data.data as OutboundReceipt;
            }
          })
      })
  }

  public onChange() {
    this.outboundReceiptService.updateOutboundReceipt(this.outboundReceipt)
      .subscribe(data => {
        if (data.isSuccess) {
          this.toastrService.success("更新成功");
        } else {
          this.toastrService.error("更新失败");
        }
      })
  }

  public returnToTable() {
    this.router.navigateByUrl('/workspace/inventory/outbound-receipt-table');
  }

  public printReceipt() {
    (<any>window).print();
  }
}