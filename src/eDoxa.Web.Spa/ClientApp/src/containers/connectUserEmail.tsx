import React, { Component } from "react";
import { connect } from "react-redux";

const connectUserEmail = WrappedComponent => {
  class Container extends Component {
    render() {
      return <WrappedComponent {...this.props} />;
    }
  }

  const mapStateToProps = () => {
    return {
      email: "admin@edoxa.gg"
    };
  };

  return connect(mapStateToProps)(Container);
};

export default connectUserEmail;
