import React, { Component } from "react";
import { connect } from "react-redux";

const connectUserPhoneNumber = WrappedComponent => {
  class Container extends Component {
    render() {
      return <WrappedComponent {...this.props} />;
    }
  }

  const mapStateToProps = () => {
    return {
      phoneNumber: "4301233494"
    };
  };

  return connect(mapStateToProps)(Container);
};

export default connectUserPhoneNumber;
