import { Pipe, PipeTransform } from "@angular/core";
import { FuseUtils } from "@fuse/utils";
import { ChatService } from 'app/apps/chat/chat.service';
import { from, Subject } from 'rxjs';
import { map } from 'rxjs/operators';

@Pipe({ name: "filterGroup",pure: false })
export class filterGroupPipe implements PipeTransform {
    /**
     * Transform
     *
     * @param {any[]} mainArr
     * @param {string} searchText
     * @param {string} property
     * @returns {any}
     */


    constructor(private _chatService: ChatService) {

    }
    
    transform(groups: any[], searchText: string, property: string): any {
        
        return groups.filter(g=>g.name.includes(searchText))
    }
}