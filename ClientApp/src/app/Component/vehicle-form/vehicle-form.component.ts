import { SaveVehicle, vehicle } from './../../Models/vehicle';
import { VehicleService } from '../../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/forkJoin'


@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
makes:any[];
models : any[];
vehicle: SaveVehicle = {
  id:0,
  makeId :0,
  modelId :0,
  isRegistered : false,
  features : [],
  contact:{
  name: '',
  phone : '',
  email : ''
}
};
features : any[];
  constructor(private vehicleService:VehicleService,
    private router : Router,
    private route : ActivatedRoute) {

      route.params.subscribe(p=>{
        this.vehicle.id = +p['id'];
      });
     }

  ngOnInit() {

    var sources = [
      this.vehicleService.getMakes(),
      this.vehicleService.getFeatures()
    ];
    if(this.vehicle.id)
    sources.push(this.vehicleService.getVehicle(this.vehicle.id));
    Observable.forkJoin(sources).subscribe(data => {
      this.makes = data[0];
      this.features = data[1];
      if(this.vehicle.id)
      this.setVehicle(data[2]);
    } , err => {if(err.status == 404)
    this.router.navigate(['/home']);
  });
  }

  private setVehicle(v : vehicle)
  {
    this.vehicle.id = v.id;
    this.vehicle.makeId = v.make.id;
    this.vehicle.modelId = v.model.id;
    this.vehicle.isRegistered = v.isRegistered;
    this.vehicle.contact = v.contact;
   
  }
  onMakeChange()
  {
    var selectedMake = this.makes.find(m=>m.id == this.vehicle.makeId);
    this.models = selectedMake? selectedMake.models : [];
    delete this.vehicle.modelId;
  }
  onFeatureToggle(featureId , $event)
  {
    if($event.target.checked)
    this.vehicle.features.push(featureId);
    else
    {
      var index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index ,1);
    }
  }
  submit()
  {
    this.vehicleService.create(this.vehicle)
    .subscribe(
      x=>console.log(x));
  }
}
