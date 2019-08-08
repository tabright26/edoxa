import React, { Component } from 'react';
import { connect } from 'react-redux';

const withUserProfileHoc = WrappedComponent => {
  class UserProfileContainer extends Component {
    render() {
      return <WrappedComponent profile={this.props.profile} />;
    }
  }

  const mapStateToProps = state => {
    return {
      profile: {}
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {}
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(UserProfileContainer);
};

export default withUserProfileHoc;
