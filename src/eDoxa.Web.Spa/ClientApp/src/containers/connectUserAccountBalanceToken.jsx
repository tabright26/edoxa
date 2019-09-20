import React, { Component } from "react";
import { connect } from "react-redux";
import { loadUserAccountBalanceForToken } from "actions/cashier/creators";

const connectUserAccountBalanceToken = WrappedComponent => {
  class Container extends Component {
    componentDidMount() {
      this.props.actions.loadUserAccountBalanceForToken();
    }

    render() {
      const { currency, available, pending, ...attributes } = this.props;
      return <WrappedComponent currency={currency} available={available} pending={available} {...attributes} />;
    }
  }

  const mapStateToProps = state => {
    return {
      available: state.user.account.balance.token.available,
      pending: state.user.account.balance.token.pending,
      currency: "Token"
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
