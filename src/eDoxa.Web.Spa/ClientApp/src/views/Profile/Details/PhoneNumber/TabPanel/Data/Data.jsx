import React, { Component } from "react";

class PhoneNumberData extends Component {
  render() {
    const { phoneNumber } = this.props;
    return (
      <dl className="row mb-0">
        <dd className="col-sm-3 text-muted mb-0">Phone number</dd>
        <dd className="col-sm-9 mb-0">{phoneNumber}</dd>
      </dl>
    );
  }
}

export default PhoneNumberData;
