import React, { Component } from "react";
import { connect } from "react-redux";

const withEmail = WrappedComponent => {
  class EmailContainer extends Component {
    render() {
      return <WrappedComponent {...this.props} />;
    }
  }

  const mapStateToProps = () => {
    return {
      email: "admin@edoxa.gg"
    };
  };

  return connect(mapStateToProps)(EmailContainer);
};

export default withEmail;
