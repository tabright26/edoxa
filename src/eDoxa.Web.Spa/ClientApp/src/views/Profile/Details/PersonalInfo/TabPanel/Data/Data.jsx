import React, { Component } from "react";
import Moment from "react-moment";

import withPersonalInfoHoc from "../../withPersonalInfoHoc";

class PersonelInfoData extends Component {
  render() {
    const { personalInfo } = this.props;
    console.log(personalInfo);
    return (
      <dl className="row mb-0">
        <dd className="col-sm-3 text-muted">Name</dd>
        <dd className="col-sm-9">
          {personalInfo.firstName} {personalInfo.lastName}
        </dd>
        <dd className="col-sm-3 text-muted">Date of birth</dd>
        <dd className="col-sm-9">
          <Moment unix format="ll">
            {personalInfo.birthDate}
          </Moment>
        </dd>
        <dd className="col-sm-3 text-muted mb-0">Gender</dd>
        <dd className="col-sm-9 mb-0">{personalInfo.gender}</dd>
      </dl>
    );
  }
}

export default withPersonalInfoHoc(PersonelInfoData);
