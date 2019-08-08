import React, { Component } from "react";
import { connect } from "react-redux";

const withUserContainer = WrappedComponent => {
  class UserContainer extends Component {
    render() {
      const { user } = this.props;
      return <WrappedComponent isAuthenticated={user} user={user} />;
    }
  }

  const mapStateToProps = state => {
    return {
      user: state.oidc.user
    };
  };

  return connect(mapStateToProps)(UserContainer);
};

export default withUserContainer;
