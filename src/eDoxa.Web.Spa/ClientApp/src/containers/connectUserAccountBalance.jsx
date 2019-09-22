import React, { Component } from "react";
import { connect } from "react-redux";
import { loadUserAccountBalance } from "actions/cashier/creators";

const connectUserAccountBalance = currency => WrappedComponent => {
  class Container extends Component {
    componentDidMount() {
      this.props.actions.loadUserAccountBalance();
    }

    render() {
      const { currency, available, pending, ...attributes } = this.props;
      return <WrappedComponent currency={currency} available={available} pending={available} {...attributes} />;
    }
  }

  const mapStateToProps = state => {
    switch (currency) {
      case "money":
        return {
          available: state.user.account.balance.money.available,
          pending: state.user.account.balance.money.pending,
          currency
        };
      case "token":
        return {
          available: state.user.account.balance.token.available,
          pending: state.user.account.balance.token.pending,
          currency
        };
      default:
        throw new Error("Invalid balance currency.");
    }
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        loadUserAccountBalance: () => dispatch(loadUserAccountBalance(currency))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};

export default connectUserAccountBalance;
