import React, { Component } from 'react';
import { connect } from 'react-redux';
import { loadGameProviders } from '../../store/actions/userActions';

const withGameProviderContainer = WrappedComponent => {
  class GameProviderContainer extends Component {
    componentDidMount() {
      this.props.actions.loadGameProviders();
    }

    render() {
      return <WrappedComponent gameProviders={this.props.gameProviders} />;
    }
  }

  const mapStateToProps = state => {
    return {
      gameProviders: state.gameProviders || []
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        loadGameProviders: () => dispatch(loadGameProviders())
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(GameProviderContainer);
};

export default withGameProviderContainer;
