﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PiecesCreator))]
public class ChessGameController : MonoBehaviour
{
    private enum GameState{ Init, Play,Finished};
    [SerializeField] private BoardLayout startingBoardLayout;
    [SerializeField] private Board board;
    [SerializeField] private ChessUIManager uIManager;

    private PiecesCreator pieceCreator;
    private ChessPlayer whitePlayer;
    private ChessPlayer blackPlayer;
    private ChessPlayer activePlayer;
    private GameState state;

    private void Awake()
    {
        SetDependencies();
        CreatePlayers();
    }

    private void SetDependencies()
    {
        pieceCreator = GetComponent<PiecesCreator>();
    }

    private void CreatePlayers()
    {
        whitePlayer = new ChessPlayer(TeamColor.White, board);
        blackPlayer = new ChessPlayer(TeamColor.Black, board);
    }

    private void Start()
    {
        StartNewGame();
    }

    private void StartNewGame()
    {
        uIManager.HideUI();
        SetGameState(GameState.Init);
        board.SetDependencies(this);
        CreatePiecesFromLayout(startingBoardLayout);
        activePlayer = whitePlayer;
        GenerateAllPossiblePlayerMoves(activePlayer);
        SetGameState(GameState.Play);
    }

    private void SetGameState(GameState state)
    {
        this.state = state;
    }

    public bool IsGameInProgress()
    {
        return state == GameState.Play;
    }



    private void CreatePiecesFromLayout(BoardLayout layout)
    {
        for (int i = 0; i < layout.GetPiecesCount(); i++)
        {
            Vector2Int squareCoords = layout.GetSquareCoordsAtIndex(i);
            TeamColor team = layout.GetSquareTeamColorAtIndex(i);
            string typeName = layout.GetSquarePieceNameAtIndex(i);

            Type type = Type.GetType(typeName);
            CreatePieceAndInitialize(squareCoords, team, type);
        }
    }



    private void CreatePieceAndInitialize(Vector2Int squareCoords, TeamColor team, Type type)
    {
        Piece newPiece = pieceCreator.CreatePiece(type).GetComponent<Piece>();
        newPiece.SetData(squareCoords, team, board);

        Material teamMaterial = pieceCreator.GetTeamMaterial(team);
        newPiece.SetMaterial(teamMaterial);

        board.SetPieceOnBoard(squareCoords, newPiece);

        ChessPlayer currentPlayer = team == TeamColor.White ? whitePlayer : blackPlayer;
        currentPlayer.AddPiece(newPiece);
    }

    private void GenerateAllPossiblePlayerMoves(ChessPlayer player)
    {
        player.GenerateAllPossibleMoves();
    }

    public bool IsTeamTurnActive(TeamColor team)
    {
        return activePlayer.team == team;
    }

    public void EndTurn()
    {
        GenerateAllPossiblePlayerMoves(activePlayer);
        GenerateAllPossiblePlayerMoves(GetOpponentToPlayer(activePlayer));
        //ChangeActiveTeam();
        if(CheckIfGameIsFinished())
        {
            EndGame();
        }
        else{
            ChangeActiveTeam();
        }
    }
    public void EndGame()
    {
        uIManager.OnGameFinished(activePlayer.team.ToString());
        SetGameState(GameState.Finished);
    }

    private void ChangeActiveTeam()
    {
        activePlayer = activePlayer == whitePlayer ? blackPlayer : whitePlayer;
    }

    private ChessPlayer GetOpponentToPlayer(ChessPlayer player)
    {
        return player == whitePlayer ? blackPlayer : whitePlayer;
    }
    public bool CheckIfGameIsFinished()
    {
        Piece[] kingAttackingPieces = activePlayer.GetPiecesAttackingOppositePieceOfType<King>();
        if(kingAttackingPieces.Length > 0)
        {
            ChessPlayer oppositePlayer = GetOpponentToPlayer(activePlayer);
            Piece attackedKing = oppositePlayer.GetPiecesOfType<King>().FirstOrDefault();
            oppositePlayer.RemoveMovesEnablingAttackOnPiece<King>(activePlayer, attackedKing);

            int avalaibleKingMoves = attackedKing.avaliableMoves.Count;
            if(avalaibleKingMoves == 0)
            {
                bool canCoverKing = oppositePlayer.CanHidePieceFromAttack<King>(activePlayer);
                if(!canCoverKing)
                {
                    return true;
                }
            }


        }
        return false;
    }


    public void RemoveMovesEnablingAttackOnPiece<T>(Piece piece) where T : Piece 
    {
        activePlayer.RemoveMovesEnablingAttackOnPiece<T>(GetOpponentToPlayer(activePlayer),piece);
    }

    public void OnPieceRemoved(Piece piece)
    {
        ChessPlayer pieceOwner = (piece.team == TeamColor.White) ? whitePlayer : blackPlayer;
        pieceOwner.RemovePiece(piece);
        Destroy(piece.gameObject);
    }
    public void ExitGame()
    {
        Debug.Log("finito di giocare");
    }

    public void RestartGame()
    {
        DestroyPieces();
        board.OnGameRestarted();
        whitePlayer.OnGameRestarted();
        blackPlayer.OnGameRestarted();
        StartNewGame();
    }

    public void DestroyPieces()
    {
        whitePlayer.activePieces.ForEach(p => Destroy(p.gameObject));
        blackPlayer.activePieces.ForEach(p => Destroy(p.gameObject));

    }

    
}

