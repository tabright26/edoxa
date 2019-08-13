import React, { Component } from "react";

import withDoxaTagHoc from "../../withDoxaTagHoc";

class DoxaTagData extends Component {
  render() {
    const { doxaTag } = this.props;
    return (
      <dl className="row mb-0">
        <dd className="col-sm-3 mb-0 text-muted">DoxaTag</dd>
        <dd className="col-sm-9 mb-0">
          {doxaTag.name}#{doxaTag.code}
        </dd>
      </dl>
    );
  }
}

export default withDoxaTagHoc(DoxaTagData);
