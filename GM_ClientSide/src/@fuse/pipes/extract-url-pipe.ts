import { Pipe, PipeTransform } from '@angular/core';
import { FuseUtils } from '@fuse/utils';
import { FacebookService, LoginOptions } from 'ngx-facebook';
import { AppHelperService } from 'app/core/utilities/app-helper.service';
import { ApiMethod } from 'ngx-facebook/dist/esm/providers/facebook';
import { environment } from 'environments/environment';

@Pipe({ name: 'extractUrl' })
export class ExtractUrlPipe implements PipeTransform {
    /**
     * Transform
     *
     * @param {any[]} mainArr
     * @param {string} searchText
     * @param {string} property
     * @returns {any}
     */
    constructor(
        public _appHelperService: AppHelperService,
        private fbk: FacebookService) {


    }

    transform(message: string) {

        var res = this._appHelperService.ExtractUrl(message);

        var result: Promise<any[]>;
        if (res != null) {
            result = Promise.all(res.map(async (val): Promise<any> => {

                if (val.includes('cloudinary')) {
                    var obj = {
                        imgLink: val,
                        urlLink: null,
                        imgTitle: null
                    }
                    return obj;
                }

                var objOg = await this.getOg(val);
                if (objOg != null) {
                    var obj = {
                        imgLink: objOg.imageUrl,
                        urlLink: val,
                        imgTitle: objOg.titleUrl
                    }
                }
                return obj;
            })
            )


        }
        result = result.then(data => data.filter(v => v));

        return result;

    }
    async getOg(urlPath: string) {
        var accessToken;
        await this.fbk.getLoginStatus().then(response => {

            if (response.status === 'connected') {
                accessToken = response.authResponse.accessToken;
            }
            else {
                accessToken = environment.applicationFacebook.access_token;
            }
        });

        let obj: any;
        var apiMethod: ApiMethod = "post";

        await this.fbk.api(
            '/',
            apiMethod,
            { "access_token": accessToken, "scrape": "true", "id": urlPath }
        ).then(function (response) {


            obj = {
                imageUrl: response.image[0].url,
                titleUrl: response.title
            }

        }
        ).catch(err => {
            obj = null;
        });

        return obj;
    }
}
