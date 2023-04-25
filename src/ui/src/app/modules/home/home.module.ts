import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeRoutingModule } from './home-routing.module';
import { TextParallaxComponent } from 'src/app/shared/components/text-parallax/text-parallax.component';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { AmongusParallaxComponent } from './components/amongus-parallax/amongus-parallax.component';
import { LandingComponent } from './pages/landing/landing.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { HowToPageComponent } from './pages/how-to-page/how-to-page.component';


@NgModule({
    declarations: [
        TextParallaxComponent,
        LandingPageComponent,
        AmongusParallaxComponent,
        LandingComponent,
        HowToPageComponent
    ],
    imports: [
        CommonModule,
        HomeRoutingModule,
        SharedModule
    ]
})
export class HomeModule { }
