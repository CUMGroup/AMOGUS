import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { NavBarComponent } from "./shared/components/nav-bar/nav-bar.component";


const routes: Routes = [
    {
        path: '', component: NavBarComponent, children: [
            { path: 'home', loadChildren: () => import('./modules/home/home.module').then(e => e.HomeModule) },
            { path: 'user', loadChildren: () => import('./modules/user/user.module').then(e => e.UserModule) }
        ]
    },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
