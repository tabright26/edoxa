import React, { Component } from 'react';
import { connect } from 'react-redux';
import { loadUserAccountBalanceForMoney } from '../../../../../store/actions/userAccountActions';

const withUserAccountBalanceMoneyContainer = WrappedComponent => {
  class UserAccountBalanceMoneyContainer extends Component {
    componentDidMount() {
      this.props.actions.loadUserAccountBalanceForMoney();
    }

    render() {
      return <WrappedComponent currency={this.props.money} />;
    }
  }

  const mapStateToProps = state => {
    return {
      money: state.user.account.balance.money
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        loadUserAccountBalanceForMoney: () =>
          dispatch(loadUserAccountBalanceForMoney())
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(UserAccountBalanceMoneyContainer);
};

export default withUserAccountBalanceMoneyContainer;
