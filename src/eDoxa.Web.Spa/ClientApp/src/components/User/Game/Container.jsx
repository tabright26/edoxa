import React, { Component } from 'react';
import { connect } from 'react-redux';
import { loadUserGames } from '../../../store/actions/userGameActions';

const withUserGameContainer = WrappedComponent => {
  class UserGameContainer extends Component {
    componentDidMount() {
      this.props.actions.loadUserGames();
    }

    render() {
      return <WrappedComponent games={this.props.games} />;
    }
  }

  const mapStateToProps = state => {
    return {
      games: state.user.games
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
  )(UserGameContainer);
};

export default withUserGameContainer;
