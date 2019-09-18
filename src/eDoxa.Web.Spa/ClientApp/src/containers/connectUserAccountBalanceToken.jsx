import React, { Component } from "react";
import { connect } from "react-redux";
import { loadUserAccountBalanceForToken } from "actions/cashier/creators";

const connectUserAccountBalanceToken = WrappedComponent => {
  class Container extends Component {
    componentDidMount() {
      this.props.actions.loadUserAccountBalanceForToken();
    }

    render() {
      return <WrappedComponent currency={this.props.token} />;
    }
  }

  const mapStateToProps = state => {
    return {
      token: state.user.account.balance.token
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        loadUserAccountBalanceForToken: () => dispatch(loadUserAccountBalanceForToken())
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};

export default connectUserAccountBalanceToken;
