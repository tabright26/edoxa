import React, { Component } from "react";

class EmailData extends Component {
  render() {
    const { email } = this.props;
    return (
      <dl className="row mb-0">
        <dd className="col-sm-3 mb-0 text-muted">Email</dd>
        <dd className="col-sm-9 mb-0">{email}</dd>
      </dl>
    );
  }
}

export default EmailData;
