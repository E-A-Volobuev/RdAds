const PageHelper = {

    openBlobPage(data, filename) {

        let a = document.createElement('a');
        let url = window.URL.createObjectURL(data);

        a.href = url;
        a.download = this._getFileNameWithDateTime(filename);

        document.body.appendChild(a);

        a.click();

        document.body.removeChild(a);

        window.URL.revokeObjectURL(url);
    },

    _getFileNameWithDateTime(f) {

        var ext = f.split('.').pop();
        var name = f.substring(0, f.lastIndexOf("."));
        var dt = new Date().toLocaleString().replaceAll(' ', '_').replaceAll(',', '').replaceAll(':', '-');

        return name + '_' + dt + '.' + ext;
    },

};

export default PageHelper;