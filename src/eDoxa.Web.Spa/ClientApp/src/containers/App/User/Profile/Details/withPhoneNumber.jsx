import React, { Component } from "react";
import { connect } from "react-redux";

const withPhoneNumber = WrappedComponent => {
  class PhoneNumberContainer extends Component {
    render() {
      return <WrappedComponent {...this.props} />;
    }
  }

  const mapStateToProps = () => {
    return {
      phoneNumber: "4301233494"
    };
  };

  return connect(mapStateToProps)(PhoneNumberContainer);
};

export default withPhoneNumber;
