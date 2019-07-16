import { Component,ViewEncapsulation } from '@angular/core';

import { fuseAnimations } from '@fuse/animations';


@Component({
    selector     : 'apps',
    templateUrl  : './apps.component.html',
    encapsulation: ViewEncapsulation.None,
    animations   : fuseAnimations
})
export class AppsComponent 
{
    constructor()
    {
    }

}
