import React, { Component } from "react";
import { connect } from "react-redux";
import { loadUserAccountBalanceForMoney } from "actions/cashier/creators";

const connectUserAccountBalanceMoney = WrappedComponent => {
  class Container extends Component {
    componentDidMount() {
      this.props.actions.loadUserAccountBalanceForMoney();
    }

    render() {
      const { currency, available, pending, ...attributes } = this.props;
      return <WrappedComponent currency={currency} available={available} pending={available} {...attributes} />;
    }
  }

  const mapStateToProps = state => {
    return {
      available: state.user.account.balance.money.available,
      pending: state.user.account.balance.money.pending,
      currency: "Money"
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        loadUserAccountBalanceForMoney: () => dispatch(loadUserAccountBalanceForMoney())
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};

export default connectUserAccountBalanceMoney;
