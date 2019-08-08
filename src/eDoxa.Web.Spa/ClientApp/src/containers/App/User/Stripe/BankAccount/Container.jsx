import React, { Component } from 'react';
import { connect } from 'react-redux';
import { hasUserStripeBankAccount } from '../../../../store/actions/userAccountActions';

const withUserStripeBankAccountContainer = WrappedComponent => {
  class UserStripeBankAccountContainer extends Component {
    componentDidMount() {
      this.props.actions.hasUserStripeBankAccount();
    }

    render() {
      return <WrappedComponent hasBankAccount={this.props.hasBankAccount} />;
    }
  }

  const mapStateToProps = state => {
    return {
      hasBankAccount: state.cashier.hasBankAccount
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        hasUserStripeBankAccount: () => dispatch(hasUserStripeBankAccount())
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(UserStripeBankAccountContainer);
};

export default withUserStripeBankAccountContainer;
