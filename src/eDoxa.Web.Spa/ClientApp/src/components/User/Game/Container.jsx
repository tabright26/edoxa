import React, { Component } from 'react';
import { connect } from 'react-redux';
import { loadUserGames } from '../../../store/actions/userGameActions';

const withUserGamesContainer = WrappedComponent => {
  class UserGamesContainer extends Component {
    componentDidMount() {
      this.props.actions.loadUserGames();
    }

    render() {
      return <WrappedComponent userGames={this.props.userGames} />;
    }
  }

  const mapStateToProps = state => {
    return {
      userGames: state.gameProviders
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        loadUserGames: () => dispatch(loadUserGames())
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(UserGamesContainer);
};

export default withUserGamesContainer;
